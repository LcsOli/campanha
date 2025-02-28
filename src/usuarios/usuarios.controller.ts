import { Controller, Get, Post, Body, BadRequestException, InternalServerErrorException } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';
import { Usuario } from './entities/usuario.entity';

@Controller('usuarios')
export class UsuariosController {
  constructor(private readonly usuariosService: UsuariosService) {}

  /**
   * Criar um novo usuário
   */
  @Post()
  async create(@Body() usuario: Usuario) {
    try {
      if (!usuario.cpf || !usuario.senha || !usuario.nome) {
        throw new BadRequestException("CPF, Nome e Senha são obrigatórios!");
      }

      return await this.usuariosService.create(usuario);
    } catch (error) {
      console.error('Erro ao criar usuário:', error);
      throw new InternalServerErrorException("Erro ao criar usuário.");
    }
  }

  /**
   * Retorna todos os usuários cadastrados
   */
  @Get()
  async findAll() {
    try {
      return await this.usuariosService.findAll();
    } catch (error) {
      console.error('Erro ao buscar usuários:', error);
      throw new InternalServerErrorException("Erro ao buscar usuários.");
    }
  }

  /**
   * Endpoint para criptografar todas as senhas não criptografadas
   */
  @Post('/criptografar-senhas')
  async criptografarSenhas() {
    try {
      await this.usuariosService.criptografarSenhas();
      return { message: 'Senhas criptografadas com sucesso!' };
    } catch (error) {
      console.error('Erro ao criptografar senhas:', error);
      throw new InternalServerErrorException("Erro ao criptografar senhas.");
    }
  }

  /**
   * Endpoint de login
   */
  @Post('/login')
  async login(@Body() loginDto: { cpf: string, senha: string }) {
    try {
      console.log('?? Login body recebido:', loginDto);

      if (!loginDto.cpf || !loginDto.senha) {
        throw new BadRequestException("CPF e Senha são obrigatórios!");
      }

      return await this.usuariosService.validateUser(loginDto.cpf, loginDto.senha);
    } catch (error) {
      console.error('Erro ao fazer login:', error);
      throw new InternalServerErrorException("Erro ao fazer login.");
    }
  }
}
