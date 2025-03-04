import { Controller, Post, Body, BadRequestException, UnauthorizedException, Get, Param } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';
import { AuthService } from './auth.service';

@Controller('usuarios')
export class UsuariosController {
  constructor(
    private readonly usuariosService: UsuariosService,
    private readonly authService: AuthService,
  ) {}

  //rota para listar os usuários
  @Get('/')
  async listarUsuarios() {
    return this.usuariosService.findAll();
  }

 //rota para obter o nome do usuário
  @Get('/nome/:cpf')
  async obterNomeUsuario(@Param('cpf') cpf: string) {
    return this.usuariosService.getNomePorCpf(cpf);
  }

  //rota do login
  @Post('/login')
  async login(@Body() loginDto: { cpf: string; senha: string }) {
    try {
      console.log('🔹 Login body recebido:', loginDto);

      if (!loginDto.cpf || !loginDto.senha) {
        throw new BadRequestException("CPF e Senha são obrigatórios!");
      }

      //valida o usuário
      const user = await this.authService.validateUser(loginDto.cpf, loginDto.senha);

      // valida o usuário e gera o token
      const tokenData = await this.authService.login(user);
      return tokenData;
    } catch (error) {
      console.error('❌ Erro ao fazer login:', error);
      if (error instanceof UnauthorizedException) {
        throw error;
      }
      throw new BadRequestException("Falha ao autenticar usuário.");
    }
  }

 //rota para registrar o usuário
  @Post('/registrar')
  async registrarUsuario(@Body() usuarioDto: { cpf: string; nome: string; senha: string; grupo?: number }) {
    try {
      console.log("🔹 Tentando cadastrar usuário:", usuarioDto);
      const usuario = await this.usuariosService.create(usuarioDto);
      return { message: "Usuário cadastrado com sucesso!", usuario };
    } catch (error) {
      console.error("❌ Erro ao cadastrar usuário:", error);
      throw new BadRequestException(error.message || "Erro ao registrar usuário.");
    }
  }
}
