import { Controller, Get, Param } from '@nestjs/common';
import { VendasService } from './vendas.service';
import { Venda } from './entities/venda.entity';

@Controller('vendas')
export class VendasController {
  constructor(private readonly vendasService: VendasService) {}

  @Get()
  async getAllVendas(): Promise<Venda[]> {
    return this.vendasService.findAll();
  }

  @Get(':dtmov/:rcacode/:cgc_client')
  async getVendaById(
    @Param('dtmov') dtmov: string,
    @Param('rcacode') rcacode: number,
    @Param('cgc_client') cgc_client: string
  ): Promise<Venda | null> {
    return this.vendasService.findOne(dtmov, rcacode, cgc_client);
  }
}
