import { Entity, Column, PrimaryColumn } from "typeorm";

@Entity('fornecedor')
export class fornecedor {
    @PrimaryColumn({type: 'int'})
    codprod: number;

    @Column({type:'varchar', length:255})
    descricao: string;

    @Column({type: 'decimal', precision: 15, scale: 2})
    total:number;

    @Column({type: 'varchar', length:255 })
    fornecedor: string;

    @Column({type: 'bigint'})
    cgc:number;
    
}