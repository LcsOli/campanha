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

  async findAll(): Promise<any[]> {
    const geralList = await this.GeralRepository.find();
  
    const resultadoComCalculo = geralList.map(item => ({
      ...item,
      pontos: Number(item.faturamento) * item.point,
    }));
  
    return resultadoComCalculo;
  }
}

