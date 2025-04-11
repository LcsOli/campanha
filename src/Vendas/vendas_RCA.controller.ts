import { Controller, Get, Query, BadRequestException } from '@nestjs/common';
import { VendasRCAService } from './vendas_RCA.service';  // Alteração no nome do arquivo
import { vendaRCA } from './entities/vendaRCA.entity';

@Controller('vendasRCA')
export class VendasRCAController {
    constructor(private readonly vendasRCAService: VendasRCAService) {}

    @Get()
    async getAllVendasRCA(
        @Query('nome') nome?: string,
        @Query('dtmov') dtmov?: string,
    ): Promise<vendaRCA[]> {
        if (dtmov && !/^\d{4}-\d{2}-\d{2}$/.test(dtmov)) {
            throw new BadRequestException('Formato de data inválido. Use o formato YYYY-MM-DD');
        }
        return this.vendasRCAService.findAll(nome, dtmov);
    }
}
