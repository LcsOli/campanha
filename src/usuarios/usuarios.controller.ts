import { Controller, Post, Body, BadRequestException, UnauthorizedException, Get, Param } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';
import { AuthService } from './auth.service';

@Controller('usuarios')
export class UsuariosController {
  constructor(
    private readonly usuariosService: UsuariosService,
    private readonly authService: AuthService,
  ) {}

  /**
   * 🔹 Rota para listar todos os usuários (GET /usuarios)
   */
  @Get('/')
  async listarUsuarios() {
    return this.usuariosService.findAll();
  }

  /**
   * 🔹 Rota para obter o nome do usuário pelo CPF (GET /usuarios/nome/:cpf)
   */
  @Get('/nome/:cpf')
  async obterNomeUsuario(@Param('cpf') cpf: string) {
    return this.usuariosService.getNomePorCpf(cpf);
  }

  /**
   * 🔹 Rota para login e geração do token JWT (POST /usuarios/login)
   */
  @Post('/login')
  async login(@Body() loginDto: { cpf: string; senha: string }) {
    try {
      console.log('🔹 Login body recebido:', loginDto);

      if (!loginDto.cpf || !loginDto.senha) {
        throw new BadRequestException("CPF e Senha são obrigatórios!");
      }

      // ✅ Verificando se o usuário é válido
      const user = await this.authService.validateUser(loginDto.cpf, loginDto.senha);

      // ✅ Se o usuário for válido, gerar e retornar o token JWT + grupo do usuário
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

  /**
   * 🔹 Rota para registrar um novo usuário (POST /usuarios/registrar)
   */
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
