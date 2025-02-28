import { Controller, Get, Post, Body } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';

@Controller('usuarios')
export class UsuariosController {
  constructor(private readonly usuariosService: UsuariosService) {}

  // Rota para login (POST) - sem a criação de usuários
  @Post('/login')
  async login(@Body() loginDto: { cpf: string; password: string }) {
    return this.usuariosService.validateUser(loginDto.cpf, loginDto.password);
  }

  // Rota para listar todos os usuários (GET)
  @Get()
  findAll() {
    return this.usuariosService.findAll();
  }
}
