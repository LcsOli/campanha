import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Venda } from './entities/venda.entity';

@Injectable()
export class VendasService {
  constructor(
    @InjectRepository(Venda)
    private readonly vendasRepository: Repository<Venda>,
  ) {}

  // Buscar todas as vendas
  async findAll(): Promise<Venda[]> {
    return this.vendasRepository.find();
  }

  // Buscar uma venda específica usando a chave primária composta
  async findOne(dtmov: string, rcacode: number, cgc_client: string): Promise<Venda | null> {
    return this.vendasRepository.findOne({
      where: { dtmov, rcacode, cgc_client },
    });
  }
}
