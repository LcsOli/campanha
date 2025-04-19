import { Entity, Column, PrimaryColumn } from 'typeorm';

@Entity('diario')
export class Diario {
  @PrimaryColumn({ type: 'date' })
  dtmov: Date;

  @PrimaryColumn({ type: 'varchar', length: 255 })
  nome: string;

  @Column({ type: 'int' })
  point: number;

  @Column({ type: 'decimal', precision: 10, scale: 2 })
  total_vendido: number;

}