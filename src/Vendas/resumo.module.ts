import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ResumoVendas } from './entities/resumo.entity';
import { ResumoVendasService } from './resumo.service';
import { ResumoVendasController } from './resumo.controller';

@Module({
  imports: [TypeOrmModule.forFeature([ResumoVendas])],
  controllers: [ResumoVendasController],
  providers: [ResumoVendasService],
})
export class ResumoVendasModule {}
