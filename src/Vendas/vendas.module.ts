import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { VendasService } from './vendas.service';
import { VendasController } from './vendas.controller';
import { Venda } from './entities/venda.entity';
import { ResumoVendas } from './entities/resumo.entity';
import { ResumoVendasController } from './resumo.controller';
import { ResumoVendasService } from './resumo.service';

@Module({
  imports: [TypeOrmModule.forFeature([Venda, ResumoVendas])],
  controllers: [VendasController, ResumoVendasController],
  providers: [VendasService, ResumoVendasService],
})
export class VendasModule {}
