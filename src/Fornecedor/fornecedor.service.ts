import { Injectable } from "@nestjs/common";
import { InjectRepository } from "@nestjs/typeorm";
import { Repository } from "typeorm";
import { fornecedor } from "./entities/fornecedor.entity";

@Injectable()
export class fornecedorService {
  constructor(
    @InjectRepository(fornecedor)
    private readonly fornecedorRepository: Repository<fornecedor>,
  ) {}


  async findAll(): Promise<fornecedor[]> {
    return this.fornecedorRepository.find();
  }


  async findByCgc(cgc: number): Promise<fornecedor[]> {  
    const results = await this.fornecedorRepository.find({
      where: { cgc } 
    });

    return results.map(f => ({
      ...f,
      total: parseFloat(f.total as any) || 0 
    }));
  }

  async findByNome(nome: string): Promise<fornecedor[]> {
    const results = await this.fornecedorRepository.find({
      where: { fornecedor: nome } 
    });

    return results.map(f => ({
      ...f,
      total: parseFloat(f.total as any) || 0
    }));
  }
}
