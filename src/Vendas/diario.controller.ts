import { Controller, Get, Query } from '@nestjs/common';
import { Diario } from './entities/diario.entity';
import { DiarioService } from './diario.service';

@Controller('diario')
export class DiarioController {
  constructor(private readonly diarioService: DiarioService) {}

  @Get()
  async getAllResumoVendas(
    @Query('startDate') startDate?: string,
    @Query('endDate') endDate?: string,
    @Query('nome') nome?: string,
  ): Promise<Diario[]> {
    return this.diarioService.findAll({ startDate, endDate, nome });
  }
}
