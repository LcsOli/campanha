import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { vendasrcaService } from './vendas_RCA.service';
import { vendasrcaController } from './vendas_RCA.controller';
import { vendaRCA } from './entities/vendaRCA.entity';
@Module({
  imports: [TypeOrmModule.forFeature([vendaRCA])],
  controllers: [vendasrcaController],
  providers: [vendasrcaService],
  exports: [vendasrcaService],
})
export class vendasrcaModule {}
