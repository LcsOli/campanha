import { Entity, Column,  PrimaryGeneratedColumn } from 'typeorm';

@Entity('resumo_rca')
export class vendaRCA {
    @PrimaryGeneratedColumn({ type: 'int' })
    id: number;

    @Column({ type: 'varchar', length: 255 })
    nome: string;

    @Column({ type: 'decimal', precision: 15, scale: 2, transformer: { from: value => parseFloat(value), to: value => value } })
    vendas: number;

    @Column({ type: 'int' })
    pontos: number;

    @Column({ type: 'date' })
    dtmov: string;
}