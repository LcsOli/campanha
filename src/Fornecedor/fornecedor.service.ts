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

  // **Buscar todos os fornecedores**
  async findAll(): Promise<fornecedor[]> {
    return this.fornecedorRepository.find();
  }

  // **Buscar fornecedor por CNPJ**
  async findByCgc(cgc: number): Promise<fornecedor[]> {  
    const results = await this.fornecedorRepository.find({
      where: { cgc } 
    });

    return results.map(f => ({
      ...f,
      total: parseFloat(f.total as any) || 0 
    }));
  }

  // **Buscar fornecedor por nome**
  async findByNome(nome: string): Promise<fornecedor[]> {
    const results = await this.fornecedorRepository.find({
      where: { fornecedor: nome }  // se o campo no entity for "fornecedor"
    });

    return results.map(f => ({
      ...f,
      total: parseFloat(f.total as any) || 0
    }));
  }
}
