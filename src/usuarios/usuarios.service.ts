import { Injectable, NotFoundException, UnauthorizedException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Usuario } from './entities/usuario.entity';
import * as bcrypt from 'bcrypt';
import { JwtService } from '@nestjs/jwt';

@Injectable()
export class UsuariosService {
  constructor(
    @InjectRepository(Usuario)
    private readonly usuarioRepository: Repository<Usuario>,
    private readonly jwtService: JwtService,
  ) {}

  async create(usuario: Usuario): Promise<Usuario> {
    usuario.senha = await bcrypt.hash(usuario.senha, 10).then(hash => hash);
    return this.usuarioRepository.save(usuario);
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

  async validateUser(cpf: string, password: string): Promise<{ accessToken: string }> {
    // Buscar o usuário pelo CPF
    const usuario = await this.usuarioRepository.findOne({ where: { CPF: cpf } });
  
    if (!usuario) {
      throw new UnauthorizedException('CPF não encontrado');
    }
  
    // Comparar a senha fornecida com a armazenada (utilizando bcrypt)
    const isPasswordValid = await bcrypt.compare(password, usuario.senha);  // Aqui você compara a senha
  
    if (!isPasswordValid) {
      throw new UnauthorizedException('Senha incorreta');
    }
  
    // Gerar o payload do JWT
    const payload = { cpf: usuario.CPF, sub: usuario.id };
    
    // Gerar o accessToken com o payload
    const accessToken = this.jwtService.sign(payload);
  
    return { accessToken };
  }
  
  }

