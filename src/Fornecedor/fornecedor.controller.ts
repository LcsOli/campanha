import { Controller, Get } from "@nestjs/common";
import { fornecedor } from "./entities/fornecedor.entity";
import { fornecedorService } from "./fornecedor.service";

@Controller('fornecedor')
export class fornecedorController {
    constructor(private readonly fornecedorService: fornecedorService) {}

    @Get()
    async getForecedor(): Promise<fornecedor[]> {
        return this.fornecedorService.findAll();
    }

}