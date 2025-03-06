import { Entity, Column, PrimaryColumn } from 'typeorm';

@Entity('vendas')
export class Venda {
  @PrimaryColumn({ type: 'date' })
  dtmov: string;

  @PrimaryColumn({ type: 'int' })
  rcacode: number;

  @PrimaryColumn({ type: 'varchar', length: 20 })
  cgc_client: string;

  // @Column({ type: 'varchar', length: 50 })
  // unidade: string;

  // @Column({ type: 'char', length: 1 })
  // codoper: string;

  @Column({ type: 'decimal', precision: 15, scale: 4, transformer: { from: value => parseFloat(value), to: value => value } })
  total: number;

  @Column({ type: 'int' })
  qtde: number;

  @Column({ type: 'int' })
  codprod: number;

  @Column({ type: 'int' })
  point: number;

  @Column({ type: 'varchar', length: 255 })
  manager: string;
}