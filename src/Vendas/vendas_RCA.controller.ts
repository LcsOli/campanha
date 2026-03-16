import { Controller, Get, Query, BadRequestException } from '@nestjs/common';
import { vendasrcaService } from './vendas_RCA.service';  // Alteração no nome do arquivo
import { vendaRCA } from './entities/vendaRCA.entity';

@Controller('vendasrca')
export class vendasrcaController {
    constructor(private readonly vendasrcaService: vendasrcaService) {}

    @Get()
    async getAllvendasrca(
        @Query('nome') nome?: string,
        @Query('dtmov') dtmov?: string,
    ): Promise<vendaRCA[]> {
        if (dtmov && !/^\d{4}-\d{2}-\d{2}$/.test(dtmov)) {
            throw new BadRequestException('Formato de data inválido. Use o formato YYYY-MM-DD');
        }
        return this.vendasrcaService.findAll(nome, dtmov);
    }
}
