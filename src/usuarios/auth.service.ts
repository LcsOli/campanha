import { Injectable, Inject, forwardRef, UnauthorizedException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import * as bcrypt from 'bcryptjs';
import { UsuariosService } from '../usuarios/usuarios.service';
import { Usuario } from '../usuarios/entities/usuario.entity';

@Injectable()
export class AuthService {
  constructor(
    @Inject(forwardRef(() => UsuariosService))
    private readonly usuariosService: UsuariosService,
    private readonly jwtService: JwtService,
  ) {}

  async validateUser(cpf: string, senha: string): Promise<Usuario> {
    console.log("🔍 Validando usuário com CPF:", cpf);

    const usuario = await this.usuariosService.findByCpf(cpf);
    if (!usuario) {
      console.log("❌ CPF não encontrado no banco!");
      throw new UnauthorizedException('CPF não encontrado');
    }

    console.log("✅ Usuário encontrado:", usuario);

    const senhaValida = await bcrypt.compare(senha, usuario.senha);
    if (!senhaValida) {
      console.log("❌ Senha incorreta!");
      throw new UnauthorizedException('Senha incorreta');
    }

    await this.usuariosService.atualizarUltimoAcesso(usuario.id);

    return usuario;
  }

  async login(user: Usuario) {
    const payload = { cpf: user.cpf, sub: user.id };
    const accessToken = this.jwtService.sign(payload, { expiresIn: '1h' });

    // console.log("Login bem-sucedido! Token gerado:", accessToken);

    return {
      accessToken,
      cpf: user.cpf,
    };
  }
}
