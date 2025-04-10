import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { VendasRCAService } from './vendas_RCA.service';
import { VendasRCAController } from './vendas_RCA.controller';
import { vendaRCA } from './entities/vendaRCA.entity'; 

@Module({
  imports: [TypeOrmModule.forFeature([vendaRCA])],
  controllers: [VendasRCAController],
  providers: [VendasRCAService],
  exports: [VendasRCAService],
})
export class VendasRCAModule {}
