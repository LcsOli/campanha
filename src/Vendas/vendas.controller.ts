import { Controller, Get, Query, Param } from '@nestjs/common';
import { VendasService } from './vendas.service';
import { Venda } from './entities/venda.entity';

@Controller('vendas')
export class VendasController {
  constructor(private readonly vendasService: VendasService) {}

  // ✅ Agora suporta paginação via query params (?page=1&limit=1000)
  @Get()
  async getAllVendas(
    @Query('rcacode') rcacode?: string,
    @Query('manager') manager?: string,
    @Query('page') page: string = '1',
    @Query('limit') limit: string = '1000' // Define 1000 como padrão para cada requisição
  ): Promise<Venda[]> {
    const rcaCodeNumber = rcacode ? parseInt(rcacode, 10) : undefined;
    const pageNumber = parseInt(page, 10) || 1; // Página padrão = 1
    const limitNumber = parseInt(limit, 10) || 1000; // Limite padrão = 1000 registros por página

    return this.vendasService.findAll(rcaCodeNumber, manager, pageNumber, limitNumber);
  }

  // 🔍 Buscar uma venda específica
  @Get(':dtmov/:rcacode/:codprod/:manager')
  async getVendaById(
    @Param('dtmov') dtmov: string,
    @Param('rcacode') rcacode: number,
    @Param('codprod') codprod: number,
    @Param('manager') manager: string
  ): Promise<Venda | null> {
    return this.vendasService.findOne(dtmov, rcacode, codprod, manager);
  }
}
