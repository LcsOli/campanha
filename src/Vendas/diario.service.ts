import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Diario } from './entities/diario.entity';
import { Repository } from 'typeorm';

interface FindAllOpts {
  startDate?: string;
  endDate?: string;
  nome?: string;
}

@Injectable()
export class DiarioService {
  constructor(
    @InjectRepository(Diario)
    private readonly diarioRepository: Repository<Diario>,
  ) {}

  async findAll(opts: FindAllOpts): Promise<Diario[]> {
    const qb = this.diarioRepository.createQueryBuilder('d');

    // filtra por intervalo de datas, se fornecido
    if (opts.startDate && opts.endDate) {
      qb.andWhere(
        'd.dtmov BETWEEN :start AND :end',
        { start: opts.startDate, end: opts.endDate },
      );
    }

    // filtra por nome, se fornecido
    if (opts.nome) {
      qb.andWhere(
        'LOWER(d.nome) LIKE :nome',
        { nome: `%${opts.nome.toLowerCase()}%` },
      );
    }

    // ordena pela data de movimento
    return qb
      .orderBy('d.dtmov', 'ASC')
      .getMany();
  }
}
