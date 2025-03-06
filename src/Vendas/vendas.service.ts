import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Venda } from './entities/venda.entity';

@Injectable()
export class VendasService {
  constructor(
    @InjectRepository(Venda)
    private readonly vendaRepository: Repository<Venda>,
  ) {}

  async findAll(rcacode?: number, manager?: string, page: number = 1, limit: number = 1000): Promise<Venda[]> {
    const query = this.vendaRepository
      .createQueryBuilder('venda')
      .select(['venda.dtmov', 'venda.rcacode', 'venda.codprod', 'venda.manager', 'venda.total'])
      .orderBy('venda.dtmov', 'DESC') // 🔹 Ordena pela data mais recente primeiro
      .limit(limit)  // 🔥 Define o limite de registros por página
      .offset((page - 1) * limit); // 🔥 Pula os registros já carregados

    if (rcacode) {
      query.andWhere('venda.rcacode = :rcacode', { rcacode });
    }

    if (manager) {
      query.andWhere('venda.manager LIKE :manager', { manager: `%${manager}%` }); // 🔹 Busca parcial pelo manager
    }

    return query.getMany();
  }

  async findOne(dtmov: string, rcacode: number, codprod: number, manager: string): Promise<Venda | null> {
    return this.vendaRepository.findOne({ where: { dtmov, rcacode, codprod, manager } });
  }
}
