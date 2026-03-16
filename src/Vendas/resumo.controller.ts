import { Controller, Get } from '@nestjs/common';
import { ResumoVendasService } from './resumo.service';
import { ResumoVendas } from './entities/resumo.entity';

@Controller('resumo')
export class ResumoVendasController {
  constructor(private readonly resumoVendasService: ResumoVendasService) {}

  @Get()
  async getAllResumoVendas(): Promise<ResumoVendas[]> {
    return this.resumoVendasService.findAll();
  }
}
