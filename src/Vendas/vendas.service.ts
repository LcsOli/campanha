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

  async findAll(rcacode?: number, manager?: string): Promise<Venda[]> {
    const query = this.vendaRepository.createQueryBuilder('venda');
  
    if (rcacode) {
      query.andWhere('venda.rcacode = :rcacode', { rcacode });
    }
  
    if (manager) {
      query.andWhere('venda.manager LIKE :manager', { manager: `%${manager}%` }); 
      // 🔹 O LIKE permite busca parcial, útil se o manager não precisar ser exato
    }
  
    return query.getMany();
  }
  

  async findOne(dtmov: string, rcacode: number, cgc_client: string, manager: string): Promise<Venda | null> {
    return this.vendaRepository.findOne({ where: { dtmov, rcacode, cgc_client } });
  }
}
