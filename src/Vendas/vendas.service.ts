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

  async findAll(rcacode?: number): Promise<Venda[]> {
    const query = this.vendaRepository.createQueryBuilder('venda');

    if (rcacode) {
      query.where('venda.rcacode = :rcacode', { rcacode });
    }

    return query.getMany();
  }

  async findOne(dtmov: string, rcacode: number, cgc_client: string): Promise<Venda | null> {
    return this.vendaRepository.findOne({ where: { dtmov, rcacode, cgc_client } });
  }
}
