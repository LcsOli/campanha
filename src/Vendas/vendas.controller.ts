import { Controller, Get, Query, Param } from '@nestjs/common';
import { VendasService } from './vendas.service';
import { Venda } from './entities/venda.entity';

@Controller('vendas')
export class VendasController {
  constructor(private readonly vendasService: VendasService) {}

  // ✅ Agora aceita filtros opcionais
  @Get()
  async getAllVendas(
    @Query('rcacode') rcacode?: string,
    @Query('manager') manager?: string
  ): Promise<Venda[]> {
    const rcaCodeNumber = rcacode ? parseInt(rcacode, 10) : undefined;
    return this.vendasService.findAll(rcaCodeNumber, manager);
  }
  

  @Get(':dtmov/:rcacode/:cgc_client/:manager')
  async getVendaById(
    @Param('dtmov') dtmov: string,
    @Param('rcacode') rcacode: number,
    @Param('cgc_client') cgc_client: string,
    @Param('manager') manager: string
  ): Promise<Venda | null> {
    return this.vendasService.findOne(dtmov, rcacode, cgc_client, manager);
  }
}
