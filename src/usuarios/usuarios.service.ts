import { Injectable, NotFoundException, UnauthorizedException, BadRequestException } from '@nestjs/common';
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

//busca o usuário pelo cpf
  async findByCpf(cpf: string): Promise<Usuario | null> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf } });

    if (!usuario) {
      console.warn(`⚠️ Usuário com CPF ${cpf} não encontrado no banco.`);
      return null;
    }

    return usuario;
  }

  //obtem o nome do usuário pelo cpf
  async getNomePorCpf(cpf: string): Promise<{ nome: string }> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf }, select: ['nome'] });

    if (!usuario) {
      throw new NotFoundException(`Usuário com CPF ${cpf} não encontrado.`);
    }

    return { nome: usuario.nome };
  }

  //cria um novo usuário
  async create(usuarioDto: { cpf: string; nome: string; senha: string; grupo?: number }): Promise<Usuario> {
    console.log("🔹 Criando usuário:", usuarioDto);

    // Verifica se o CPF já existe no banco
    const usuarioExistente = await this.usuarioRepository.findOne({ where: { cpf: usuarioDto.cpf } });
    if (usuarioExistente) {
      throw new BadRequestException("CPF já cadastrado!");
    }

    // Verifica se a senha foi fornecida
    if (!usuarioDto.senha) {
      throw new BadRequestException("Senha é obrigatória!");
    }

    // Impede senhas já criptografadas de serem salvas novamente
    if (usuarioDto.senha.startsWith('$2b$')) {
      throw new BadRequestException("A senha não pode estar criptografada no cadastro!");
    }

    // Criptografa a senha antes de salvar no banco
    const usuario = new Usuario();
    usuario.cpf = usuarioDto.cpf;
    usuario.nome = usuarioDto.nome;
    usuario.senha = await bcrypt.hash(usuarioDto.senha, 10);
    

    // Salva no banco de dados
    const novoUsuario = await this.usuarioRepository.save(usuario);
    console.log("✅ Usuário criado com sucesso:", novoUsuario);

    return novoUsuario;
  }

//retorna todos os usuários
  async findAll(): Promise<Usuario[]> {
    return this.usuarioRepository.find();
  }

 //busca um usuário pelo id
  async findOne(id: number): Promise<Usuario> {
    const usuario = await this.usuarioRepository.findOne({ where: { id } });
    if (!usuario) {
      throw new NotFoundException('Usuário não encontrado');
    }
    return usuario;
  }

  //atualiza um usuário
  async validateUser(cpf: string, senhaDigitada: string): Promise<{ accessToken: string; grupo: number }> {
    console.log("🔹 Buscando usuário com CPF:", cpf);
    
    const usuario = await this.findByCpf(cpf);
    if (!usuario) {
      console.warn("⚠️ CPF não encontrado no banco!");
      throw new UnauthorizedException('CPF não encontrado');
    }

    console.log("✅ Usuário encontrado:", usuario.nome);

    const isPasswordValid = await bcrypt.compare(senhaDigitada, usuario.senha);
    if (!isPasswordValid) {
      console.warn("⚠️ Senha incorreta para CPF:", cpf);
      throw new UnauthorizedException('Senha incorreta');
    }

    
   //atuazliza o ultimo acesso do usuário
    console.log("🛠️ Tentando atualizar último acesso...");

    const resultado = await this.usuarioRepository
      .createQueryBuilder()
      .update(Usuario)
      .set({ acesso: () => 'NOW()' })
      .where("id = :id", { id: usuario.id })
      .execute();
    
    console.log(`✅ Query de atualização executada. Linhas afetadas: ${resultado.affected}`);
    
    // gerar o token de acesso
    const payload = { cpf: usuario.cpf, sub: usuario.id, grupo: usuario.grupo };
    const accessToken = this.jwtService.sign(payload);
    console.log("✅ Login bem-sucedido! Token gerado:", accessToken);

    return { accessToken, grupo: usuario.grupo };
  }

  //criptografa as senhas
  async criptografarSenhas() {
    const usuarios = await this.usuarioRepository.find();
    for (const usuario of usuarios) {
      if (usuario.senha && !usuario.senha.startsWith('$2b$')) {
        usuario.senha = await bcrypt.hash(usuario.senha, 10);
        await this.usuarioRepository.save(usuario);
        console.log(`🔹 Senha criptografada para o usuário ${usuario.cpf}`);
      }
    }
    
  }
  async atualizarUltimoAcesso(id: number): Promise<void> {
    console.log(`🛠 Atualizando último acesso para o usuário com ID: ${id}`);

    const resultado = await this.usuarioRepository
      .createQueryBuilder()
      .update(Usuario)
      .set({ acesso: () => 'NOW()' }) 
      .where("id = :id", { id })
      .execute();

    console.log(`✅ Último acesso atualizado! Linhas afetadas: ${resultado.affected}`);
}
}
