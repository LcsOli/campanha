import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { fornecedor } from "./entities/fornecedor.entity";
import { fornecedorController } from "./fornecedor.controller";
import { fornecedorService } from "./fornecedor.service";

@Module({
    imports: [TypeOrmModule.forFeature([fornecedor])],
    controllers: [fornecedorController],
    providers: [fornecedorService],
})
export class fornecedorModule {}