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

  async findByCpf(cpf: string): Promise<Usuario | null> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf } });

    if (!usuario) {
      console.warn(`⚠️ Usuário com CPF ${cpf} não encontrado no banco.`);
      return null;
    }

    return usuario;
  }

  async getNomePorCpf(cpf: string): Promise<{ nome: string }> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf }, select: ['nome'] });

    if (!usuario) {
      throw new NotFoundException(`Usuário com CPF ${cpf} não encontrado.`);
    }

    return { nome: usuario.nome };
  }

  async create(usuarioDto: { cpf: string; nome: string; senha: string; grupo?: number }): Promise<Usuario> {
    console.log("🔹 Criando usuário:", usuarioDto);

    const usuarioExistente = await this.usuarioRepository.findOne({ where: { cpf: usuarioDto.cpf } });
    if (usuarioExistente) {
      throw new BadRequestException("CPF já cadastrado!");
    }

    if (!usuarioDto.senha) {
      throw new BadRequestException("Senha é obrigatória!");
    }

    if (usuarioDto.senha.startsWith('$2b$')) {
      throw new BadRequestException("A senha não pode estar criptografada no cadastro!");
    }

    const usuario = new Usuario();
    usuario.cpf = usuarioDto.cpf;
    usuario.nome = usuarioDto.nome;
    usuario.senha = await bcrypt.hash(usuarioDto.senha, 10);

    const novoUsuario = await this.usuarioRepository.save(usuario);
    console.log("✅ Usuário criado com sucesso:", novoUsuario);

    return novoUsuario;
  }

  async findAll(): Promise<Usuario[]> {
    return this.usuarioRepository.find();
  }

  async findOne(id: number): Promise<Usuario> {
    const usuario = await this.usuarioRepository.findOne({ where: { id } });
    if (!usuario) {
      throw new NotFoundException('Usuário não encontrado');
    }
    return usuario;
  }

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

    console.log("🛠️ Tentando atualizar último acesso...");

    const resultado = await this.usuarioRepository
      .createQueryBuilder()
      .update(Usuario)
      .set({ acesso: () => 'NOW()' })
      .where("id = :id", { id: usuario.id })
      .execute();
    
    console.log(`✅ Query de atualização executada. Linhas afetadas: ${resultado.affected}`);
    
    const payload = { cpf: usuario.cpf, sub: usuario.id, grupo: usuario.grupo };
    const accessToken = this.jwtService.sign(payload);
    console.log("✅ Login bem-sucedido! Token gerado:", accessToken);

    return { accessToken, grupo: usuario.grupo };
  }

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

  async atualizarSenha(cpf: string, novaSenhaCriptografada: string): Promise<boolean> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf } });

    if (!usuario) {
      console.warn(`⚠️ Usuário com CPF ${cpf} não encontrado para atualização de senha.`);
      return false;
    }

    usuario.senha = novaSenhaCriptografada;
    await this.usuarioRepository.save(usuario);

    console.log(`✅ Senha atualizada com sucesso para CPF: ${cpf}`);
    return true;
  }

  private getSemanaDoAno(date: Date): number {
    const oneJan = new Date(date.getFullYear(), 0, 1);
    const numberOfDays = Math.floor((date.getTime() - oneJan.getTime()) / (24 * 60 * 60 * 1000));
    return Math.ceil((numberOfDays + oneJan.getDay() + 1) / 7);
  }
}
