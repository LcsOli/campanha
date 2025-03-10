import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Diario } from './entities/diario.entity';
import { Repository } from 'typeorm';

@Injectable()
export class DiarioService {
     constructor(
        @InjectRepository(Diario)
        private readonly DiarioRepository: Repository<Diario>,
      ) {}

   async findAll(): Promise<Diario[]> {
      return this.DiarioRepository.find();
    }
}