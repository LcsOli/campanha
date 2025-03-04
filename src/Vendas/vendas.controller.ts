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

  @Get(':id')
  async getVendaById(@Param('id') id: number): Promise<Venda | null> {
    return this.vendasService.findOne(id);
  }
}
