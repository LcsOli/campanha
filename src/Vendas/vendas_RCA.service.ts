import { Injectable, InternalServerErrorException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { vendaRCA } from './entities/vendaRCA.entity';

@Injectable()
export class vendasrcaService {
    constructor(
        @InjectRepository(vendaRCA)
        private readonly vendaRCARepository: Repository<vendaRCA>,
    ) { }

    async findAll(nome?: string, dtmov?: string): Promise<vendaRCA[]> {
        const query = this.vendaRCARepository
            .createQueryBuilder('vendaRCA')
            .select(['vendaRCA.nome', 'vendaRCA.vendas', 'vendaRCA.pontos', 'vendaRCA.dtmov'])
            .orderBy('vendaRCA.dtmov', 'DESC');

        if (nome) {
            query.andWhere('LOWER(vendaRCA.nome) LIKE LOWER(:nome)', {
                nome: `%${nome.toLowerCase()}%`,
            });
        }

        if (dtmov) {
            query.andWhere('vendaRCA.dtmov = :dtmov', { dtmov });
        }

    try {
  return await query.getMany();
} catch (error) {
  console.error('❌ ERRO AO EXECUTAR QUERY /vendasrca');
  console.error('Mensagem:', error.message);
  console.error('Stack:', error.stack);
  console.error('Erro completo:', error);
  throw new InternalServerErrorException('Erro interno ao buscar vendas RCA');
}
    }
}