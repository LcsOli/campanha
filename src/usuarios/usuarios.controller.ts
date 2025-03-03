import { Controller, Post, Body, BadRequestException, UnauthorizedException } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';
import { AuthService } from './auth.service';

@Controller('usuarios')
export class UsuariosController {
  constructor(
    private readonly usuariosService: UsuariosService,
    private readonly authService: AuthService,
  ) {}

  @Post('/login')
  async login(@Body() loginDto: { cpf: string; senha: string }) {
    try {
      console.log('?? Login body recebido:', loginDto);

      if (!loginDto.cpf || !loginDto.senha) {
        throw new BadRequestException("CPF e Senha são obrigatórios!");
      }

      // ✅ Verificando se o usuário é válido
      const user = await this.authService.validateUser(loginDto.cpf, loginDto.senha);

      // ✅ Se o usuário for válido, gerar e retornar o token JWT
      return this.authService.login(user);
    } catch (error) {
      console.error('❌ Erro ao fazer login:', error);
      if (error instanceof UnauthorizedException) {
        throw error;
      }
      throw new BadRequestException("Falha ao autenticar usuário.");
    }
  }
}
