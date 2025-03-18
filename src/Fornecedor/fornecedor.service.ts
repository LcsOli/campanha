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

    // **Buscar fornecedor por cnpj
    async findByCgc(cgc: number): Promise<fornecedor[]> {  
        const results = await this.fornecedorRepository.find({
            where: { cgc } 
        });
    
        return results.map(fornecedor => ({
            ...fornecedor,
            total: parseFloat(fornecedor.total as any) || 0 
        }));
    }
    
}
