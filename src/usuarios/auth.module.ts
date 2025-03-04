import { Module, forwardRef } from '@nestjs/common';
import { AuthService } from './auth.service';
import { UsuariosModule } from '../usuarios/usuarios.module';
import { JwtModule } from '@nestjs/jwt';

@Module({
    imports: [
      JwtModule.register({
        secret: 'seuSegredoAqui',
        signOptions: { expiresIn: '1h' },
      }),
      forwardRef(() => UsuariosModule),
    ],
    providers: [AuthService], 
    exports: [AuthService],
  })
  export class AuthModule {}
  

