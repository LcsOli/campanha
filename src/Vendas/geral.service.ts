import { Injectable } from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { Geral } from "./entities/geral.entity";
import { Repository } from 'typeorm';


@Injectable()
export class GeralService {
  constructor(
    @InjectRepository(Geral)
    private readonly GeralRepository: Repository<Geral>,
  ){}

  async findAll(): Promise<Geral[]> {
    return this.GeralRepository.find(); 
  }
}
