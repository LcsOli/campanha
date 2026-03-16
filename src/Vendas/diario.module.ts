import { Module } from '@nestjs/common';
import { TypeOrmModule } from "@nestjs/typeorm";
import { Diario } from "./entities/diario.entity";
import { DiarioService } from "./diario.service";
import { DiarioController } from './diario.controller';


@Module({
  imports: [TypeOrmModule.forFeature([Diario])],
  controllers: [DiarioController],
  providers: [DiarioService],
})
export class DiarioModule {}