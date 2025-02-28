import { Module } from '@nestjs/common';
import { UsuariosService } from './usuarios.service';
import { UsuariosController } from './usuarios.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Usuario } from './entities/usuario.entity';
import { JwtModule } from '@nestjs/jwt'; // Importe o JwtModule

@Module({
  imports: [
    TypeOrmModule.forFeature([Usuario]), // Adicione a entidade Usuario aqui
    JwtModule.register({
      secret: 'sua-chave-secreta', // Defina sua chave secreta aqui
      signOptions: { expiresIn: '60s' }, // Tempo de expiração do token, ajuste conforme necessário
    }),
  ],
  controllers: [UsuariosController],
  providers: [UsuariosService],
})
export class UsuariosModule {}
