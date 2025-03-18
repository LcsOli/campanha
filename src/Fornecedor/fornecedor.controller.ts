import { Controller, Get, Query } from "@nestjs/common";
import { fornecedorService } from "./fornecedor.service";

@Controller('fornecedor')
export class fornecedorController {
    constructor(private readonly fornecedorService: fornecedorService) {}

    @Get()
async getFornecedores(@Query("cgc") cgc?: string) {
    if (cgc) {
        return this.fornecedorService.findByCgc(Number(cgc)); 
    }
    return this.fornecedorService.findAll();
}

}
