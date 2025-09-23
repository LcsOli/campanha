import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Usuario } from './usuarios/entities/usuario.entity';
import { UsuariosModule } from './usuarios/usuarios.module';
import { VendasModule } from './Vendas/vendas.module';
import { Venda } from './Vendas/entities/venda.entity';
import { ResumoVendas } from './Vendas/entities/resumo.entity';
import { ResumoVendasModule } from './Vendas/resumo.module';
import { Diario } from './Vendas/entities/diario.entity';
import { DiarioModule } from './Vendas/diario.module';
import { GeralModule } from './Vendas/geral.module';
import { Geral } from './Vendas/entities/geral.entity';
import { fornecedor } from './Fornecedor/entities/fornecedor.entity';
import { fornecedorModule } from './Fornecedor/fornecedor.module';
import { vendaRCA } from './Vendas/entities/vendaRCA.entity';
import { vendasrcaModule } from './Vendas/vendas_RCA.module';

@Module({
  imports: [
    TypeOrmModule.forRoot({
      type: 'mysql',
      host: 'localhost',
      port: 3306,
      username: 'root',
      password: '135713Mudar@',
      database: 'campanha',
      entities: [Usuario, Venda, ResumoVendas, Diario, Geral, fornecedor,vendaRCA],
      synchronize: false,
      logging: true,
    }),
    UsuariosModule,
    VendasModule,
    ResumoVendasModule,
    DiarioModule,
    GeralModule,
    fornecedorModule,
    vendasrcaModule,
  ],
})
export class AppModule {}
