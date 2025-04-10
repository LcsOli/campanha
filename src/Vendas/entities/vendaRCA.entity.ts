import { Entity, Column, PrimaryColumn } from 'typeorm';

@Entity('vendaRCA')
export class vendaRCA {
    @PrimaryColumn({ type: 'varchar', length: 255 })
    nome: string;

    @Column({ type: 'decimal', precision: 15, scale: 4, transformer: { from: value => parseFloat(value), to: value => value } })
    vendas: number;

    @Column({ type: 'int' })
    pontos: number;

    @Column({ type: 'date' })
    dtmov: string;
}