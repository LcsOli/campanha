import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity('vendas') // Nome da tabela no banco
export class Venda {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ type: 'date' })
  dtmov: string;

  @Column({ type: 'varchar', length: 50 })
  unidade: string;

  @Column({ type: 'char', length: 1 })
  codoper: string;

  @Column({ type: 'decimal', precision: 15, scale: 4 })
  total: number;

  @Column({ type: 'int' })
  rcacode: number;

  @Column({ type: 'int' })
  qtde: number;

  @Column({ type: 'varchar', length: 255 })
  manager: string;

  @Column({ type: 'varchar', length: 20 })
  cgc_client: string;
}
