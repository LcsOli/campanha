import { Controller, Get, Query } from "@nestjs/common";
import { fornecedorService } from "./fornecedor.service";
import { fornecedor } from "./entities/fornecedor.entity";

@Controller('fornecedor')
export class fornecedorController {
  constructor(private readonly fornecedorService: fornecedorService) {}

  @Get()
  async getFornecedores(
    @Query('nome') nome?: string,
    @Query('cgc') cgc?: string,
  ): Promise<fornecedor[]> {
    if (nome) {
      // Busca exata por nome; use QueryBuilder para LIKE se quiser busca parcial
      return this.fornecedorService.findByNome(nome);
    }
    if (cgc) {
      return this.fornecedorService.findByCgc(Number(cgc));
    }
    return this.fornecedorService.findAll();
  }
}
