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

  async findAll(): Promise<ResumoVendas[]> {
    return this.resumoVendasRepository.find();
  }
}
