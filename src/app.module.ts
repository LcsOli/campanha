import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Usuario } from './usuarios/entities/usuario.entity';
import { UsuariosModule } from './usuarios/usuarios.module';
import { VendasModule } from './Vendas/vendas.module';

@Module({
  imports: [
    TypeOrmModule.forRoot({
      type: 'mysql',
      host: 'localhost',
      port: 3306,
      username: 'root',
      password: '123Mudar@',
      database: 'Campanha',
      entities: [Usuario],
      synchronize: false,
      logging: true,
    }),
    UsuariosModule,
    VendasModule,
  ],
})
export class AppModule {}
