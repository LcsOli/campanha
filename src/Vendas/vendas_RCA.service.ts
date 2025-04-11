import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { vendaRCA } from './entities/vendaRCA.entity';

@Injectable()
export class VendasRCAService {
    constructor(
        @InjectRepository(vendaRCA)
        private readonly vendaRCARepository: Repository<vendaRCA>,
    ) {}

    async findAll(nome?: string, dtmov?: string): Promise<vendaRCA[]> {
        const query = this.vendaRCARepository
            .createQueryBuilder('vendaRCA')
            .select(['vendaRCA.nome', 'vendaRCA.vendas', 'vendaRCA.pontos', 'vendaRCA.dtmov'])
            .orderBy('vendaRCA.dtmov', 'DESC');
    
        if (nome) {
            query.andWhere('vendaRCA.nome LIKE :nome', { nome: `%${nome}%` });
        }
    
        if (dtmov) {
           
            query.andWhere('vendaRCA.dtmov = :dtmov', { dtmov });
            return await query.getMany();
        }

        return await query.getMany();
    }
}
