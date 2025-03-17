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
}