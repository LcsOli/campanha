import { Entity, Column, PrimaryColumn } from 'typeorm';

@Entity('vendas') // Nome exato da tabela no banco
export class Venda {
  @PrimaryColumn({ type: 'date' })
  dtmov: string; // Data da venda como parte da chave primária

  @PrimaryColumn({ type: 'int' })
  rcacode: number; // Código do RCA como parte da chave primária

  @PrimaryColumn({ type: 'varchar', length: 20 })
  cgc_client: string; // Cliente como parte da chave primária

  @Column({ type: 'varchar', length: 50 })
  unidade: string;

  @Column({ type: 'char', length: 1 })
  codoper: string;

  @Column({ type: 'decimal', precision: 15, scale: 4 })
  total: number;

  @Column({ type: 'int' })
  qtde: number;

  @Column({ type: 'varchar', length: 255 })
  manager: string;
}
