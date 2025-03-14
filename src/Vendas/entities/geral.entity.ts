import { Entity, Column, PrimaryColumn } from 'typeorm';

@Entity('Geral')
export class Geral {
    @PrimaryColumn({ type: 'int' })
    rcacode: number;

    @PrimaryColumn({ type: 'varchar', length: 255 })
    nome: string;

    @Column({ type: 'int' })
    codsupervisor : number;

    @Column({ type: 'varchar', length: 255 })
    manager : string;

    @Column({ type: 'int' })
    point: number;

    @Column({ type: 'decimal', precision: 10, scale: 2 })
    faturamento: number;

    @Column({ type: 'int' })
    cupons: number;

    @Column({ type: 'int' })
    pontos: number;
}