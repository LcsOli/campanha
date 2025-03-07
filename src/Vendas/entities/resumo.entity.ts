import { Entity, Column, PrimaryColumn } from 'typeorm';

@Entity('resumo_vendas')
export class ResumoVendas {
  @PrimaryColumn({ type: 'varchar', length: 255 })
  nome: string;

  @Column({ type: 'int' })
  pontos: number;

  @Column({ type: 'decimal', precision: 10, scale: 2 })
  total_vendido: number;
}
