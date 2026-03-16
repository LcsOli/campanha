import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Geral } from './entities/geral.entity';
import { Usuario } from 'src/usuarios/entities/usuario.entity';
import { GeralService } from './geral.service';
import { GeralController } from './geral.controller';

@Module({
  imports: [TypeOrmModule.forFeature([Geral, Usuario])],
  providers: [GeralService],
  controllers: [GeralController],
})
export class GeralModule {}
