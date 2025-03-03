import { Injectable, NotFoundException, UnauthorizedException, BadRequestException, InternalServerErrorException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Usuario } from './entities/usuario.entity';
import * as bcrypt from 'bcryptjs';
import { JwtService } from '@nestjs/jwt';

@Injectable()
export class UsuariosService {
  constructor(
    @InjectRepository(Usuario)
    private readonly usuarioRepository: Repository<Usuario>,
    private readonly jwtService: JwtService,
  ) {}

  /**
   * Busca um usuário pelo CPF
   */
  async findByCpf(cpf: string): Promise<Usuario | null> {
    return await this.usuarioRepository.findOne({ where: { cpf } }) || null;
  }

  /**
   * Criar um novo usuário garantindo que a senha seja criptografada antes de salvar
   */
  async create(usuario: Usuario): Promise<Usuario> {
    console.log("?? Criando usuário:", usuario);

    if (!usuario.senha) {
      throw new BadRequestException("Senha é obrigatória!");
    }

    if (usuario.senha.startsWith('$2b$')) {
      throw new BadRequestException("A senha não pode estar criptografada no cadastro!");
    }

    usuario.senha = await bcrypt.hash(usuario.senha, 10);
    const novoUsuario = await this.usuarioRepository.save(usuario);
    console.log("? Usuário criado com sucesso:", novoUsuario);

    return novoUsuario;
  }

  /**
   * Retorna todos os usuários do banco
   */
  async findAll(): Promise<Usuario[]> {
    return this.usuarioRepository.find();
  }

  /**
   * Busca um único usuário pelo ID
   */
  async findOne(id: number): Promise<Usuario> {
    const usuario = await this.usuarioRepository.findOne({ where: { id } });
    if (!usuario) {
      throw new NotFoundException('Usuário não encontrado');
    }
    return usuario;
  }

  /**
   * Método para login e geração do token JWT
   */
  async validateUser(cpf: string, senhaDigitada: string): Promise<{ accessToken: string }> {
    console.log("?? Buscando usuário com CPF:", cpf);
    
    const usuario = await this.findByCpf(cpf);
    if (!usuario) {
      console.log("? CPF não encontrado no banco!");
      throw new UnauthorizedException('CPF não encontrado');
    }

    console.log("? Usuário encontrado:", usuario);
    console.log("?? Senha armazenada no banco:", usuario.senha);
    console.log("?? Senha digitada pelo usuário:", senhaDigitada);

    const isPasswordValid = await bcrypt.compare(senhaDigitada, usuario.senha);
    if (!isPasswordValid) {
      console.log("? Senha incorreta!");
      throw new UnauthorizedException('Senha incorreta');
    }

    const payload = { cpf: usuario.cpf, sub: usuario.id };
    const accessToken = this.jwtService.sign(payload);
    console.log("? Login bem-sucedido! Token gerado:", accessToken);
    return { accessToken };
  }

  /**
   * Método para criptografar todas as senhas que ainda não foram criptografadas no banco.
   */
  async criptografarSenhas() {
    const usuarios = await this.usuarioRepository.find();
    for (const usuario of usuarios) {
      if (usuario.senha && !usuario.senha.startsWith('$2b$')) {
        usuario.senha = await bcrypt.hash(usuario.senha, 10);
        await this.usuarioRepository.save(usuario);
        console.log(`? Senha criptografada para o usuário ${usuario.cpf}`);
      }
    }
  }
}