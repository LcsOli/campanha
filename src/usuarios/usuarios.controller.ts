import { Controller, Post, Body, BadRequestException, UnauthorizedException, Get, Param, Put } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';
import { AuthService } from './auth.service';
import * as bcrypt from 'bcryptjs';

@Controller('usuarios')
export class UsuariosController {
  constructor(
    private readonly usuariosService: UsuariosService,
    private readonly authService: AuthService,
  ) {}

  @Get('/')
  async listarUsuarios() {
    return this.usuariosService.findAll();
  }

  @Get('/nome/:cpf')
  async obterNomeUsuario(@Param('cpf') cpf: string) {
    return this.usuariosService.getNomePorCpf(cpf);
  }

  @Post('/login')
  async login(@Body() loginDto: { cpf: string; senha: string }) {
      try {
          console.log('🔹 Login body recebido:', loginDto);
  
          if (!loginDto.cpf || !loginDto.senha) {
              throw new BadRequestException("CPF e Senha são obrigatórios!");
          }

          const user = await this.authService.validateUser(loginDto.cpf, loginDto.senha);

          const accessToken = await this.authService.login(user);

          return {
              accessToken,
              user: {
                  id: user.id,
                  cpf: user.cpf,
                  nome: user.nome,
                  grupo: user.grupo, 
                  acesso: user.acesso,
                  participacao: user.participacao,
                  supervisor: user.supervisor,
              }
          };
      } catch (error) {
          console.error('❌ Erro ao fazer login:', error);
          if (error instanceof UnauthorizedException) {
              throw error;
          }
          throw new BadRequestException("Falha ao autenticar usuário.");
      }
  }
  

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

  @Put('/atualizar-senhas')
  async atualizarSenhas(@Body() usuarios: { cpf: string; novaSenha: string }[]) {
    try {
      console.log("🔹 Atualizando múltiplas senhas...");

      if (!Array.isArray(usuarios) || usuarios.length === 0) {
        throw new BadRequestException("A lista de usuários não pode estar vazia!");
      }

      const atualizacoes = await Promise.all(
        usuarios.map(async (usuario) => {
          if (!usuario.cpf || !usuario.novaSenha) {
            return { cpf: usuario.cpf, status: "Erro", message: "CPF e nova senha são obrigatórios!" };
          }

          // Criptografar a nova senha com bcrypt
          const hashedPassword = await bcrypt.hash(usuario.novaSenha, 10);

          const atualizado = await this.usuariosService.atualizarSenha(usuario.cpf, hashedPassword);

          if (!atualizado) {
            return { cpf: usuario.cpf, status: "Erro", message: "Usuário não encontrado!" };
          }

          return { cpf: usuario.cpf, status: "Sucesso", message: "Senha atualizada com sucesso!" };
        })
      );

      return { message: "Processo concluído!", resultados: atualizacoes };
    } catch (error) {
      console.error("❌ Erro ao atualizar senhas:", error);
      throw new BadRequestException(error.message || "Erro ao atualizar senhas.");
    }
  }
}
