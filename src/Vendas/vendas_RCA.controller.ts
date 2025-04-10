import { Controller, Get, Query, Param } from '@nestjs/common';
import { VendasRCAService } from './vendas_RCA.service';
import { vendaRCA } from './entities/vendaRCA.entity';

@Controller('vendasRCA')
export class VendasRCAController {
    constructor(private readonly vendasRCAService: VendasRCAService) {}

@Get()
async getAllVendasRCA(
    @Query('nome ') nome?: string,
    @Query('dtmov') dtmov?: string,
    @Query('page') page: string = '1',
    @Query('limit') limit: string = '1000' 

): Promise<vendaRCA[]> {
    const nomeString = nome ? nome.toString() : undefined;
    const pageNumber = parseInt(page, 10) || 1;
    const limitNumber = parseInt(limit, 10) || 1000; 
    return this.vendasRCAService.findAll(nomeString, dtmov, pageNumber, limitNumber);
}
}