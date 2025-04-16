import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { ResumoVendas } from './entities/resumo.entity';

@Injectable()
export class ResumoVendasService {
  constructor(
    @InjectRepository(ResumoVendas)
    private readonly resumoVendasRepository: Repository<ResumoVendas>,
  ) {}

  async findAll(): Promise<any[]> {
    const dados = await this.resumoVendasRepository.find();

    return dados.map((item) => ({
      ...item,
      pontosFormatado: (item.pontos ?? 0).toLocaleString('pt-BR'),
      totalVendidoFormatado: (item.total_vendido ?? 0).toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2,
      }),
    }));
  }
}
